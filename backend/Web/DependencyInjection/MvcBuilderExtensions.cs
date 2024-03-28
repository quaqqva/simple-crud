using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Backend.Web.DependencyInjection;

public static class MvcBuilderExtension
{
    public static void ConfigureJsonSerializationOptions(this IMvcBuilder builder)
    {
        builder
            .AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler =
                        ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.DefaultIgnoreCondition =
                        JsonIgnoreCondition.WhenWritingDefault;
                }
            )
            .ConfigureApiBehaviorOptions(
                options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var modelState = actionContext.ModelState;
                        if (modelState.ContainsKey("$"))
                        {
                            ModelStateEntry errorEntry;
                            modelState.TryGetValue("$", out errorEntry!);

                            var errorText = errorEntry.Errors.First().ErrorMessage;
                            var missingProperties = errorText.Split(':')[1].Split(',');
                            foreach (var property in missingProperties)
                            {
                                var formattedProperty = property.Trim();
                                modelState.AddModelError(
                                    formattedProperty,
                                    "Property is missing"
                                );
                            }

                            modelState.Remove("$");
                        }

                        if (modelState.ContainsKey("dto"))
                            modelState.Remove("dto");
                        var result = new ValidationProblemDetails(modelState)
                        {
                            Type = "https://courselibrary.com/modelvalidationproblem",
                            Title = "One or more model validation errors occurred."
                        };
                        result.Extensions.Add(
                            "traceId",
                            actionContext.HttpContext.TraceIdentifier
                        );
                        return new BadRequestObjectResult(result);
                    };
                }
            );
    }
}