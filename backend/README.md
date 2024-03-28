# Database API

## Avaliable endpoints

- ### `/addresses`
  Includes operations with the following **Address**  entity:
  ```
    id: number,
    country: string,
    city: string,
    street: string,
    building: string,
    customers: Customer[]
  ```
- ### `/chiefs`
  Includes operations with the following **Chief**  entity:
  ```
    id: number,
    firstName: string,
    lastName: string,
    patronymic: string | undefined,
    workshops: Workshop[]
  ```
- ### `/contracts`
  Includes operations with the following **Contract**  entity:
  ```
    id: number,
    completionDate: string,
    registrationDate: string | undefined,
    customerId: number,
    customer: Customer,
    orders: Order[]
  ```
- ### `/orders`
  Includes operations with the following **Order** entity:
  ```
    id: number,
    productQuantity: number,
    productId: number,
    contractId: number,
    contract: Contract,
    product: Product
  ```
- ### `/products`
  Includes operations with the following **Product** entity:
  ```
    id: number,
    name: string,
    price: number,
    workshopId: number,
    orders: Order[],
    workshop: Workshop
  ```
- ### `/workshops`
  Includes operations with the following **Workshop** entity:
  ```
    id: number,
    name: string,
    phoneNumber: string,
    chiefId: number,
    chief: Chief,
    products: Product[]
  ```

## Methods

- ### ```GET```
  Response body will contain an array of specified entities.<br>
  Properties that include related entities as objects are not included by default, you can get them if you
  specify ```fields``` param.
  #### Query parameters
    - ```fields```: properties that should be included in response body for each entity separated by comma. If not
      specified, entities will include all properties but those that represent related entities (
      ex. ```products```, ```chief``` for ```Workshop``` entity). Example: ```fields=id,firstName```
    - ```filter```: a filter string containing base expressions that can be wrapped in brackets, joined
      by ```AND```, ```OR```,<br>
      negated with ```NOT```.

  Base expression looks like following: ```{propertyName} {operation} {value}``` (spaces are required), where<br>
  ```operation``` depends on property's type:
    - all comparison operators (```==```) for numbers and dates<br>
    - ```==```, ```!=```, `contains`, `startWith`, `endsWith` for strings

  ```value``` is a constant that should be the same type as property. Strings should be wrapped in quotes (```'```).
  Dates should be in format ```DD-MM-YYYY``` and wrapper in quotes too.

  Example url with filter: ```/contracts?filter=not (id < 5) and (completionDate > '17-02-2004')```
    - ```sortBy```: properties that should be used to determine sort order of array separated by comma. Order of
      properties determines their priority, for example ```sortBy=firstName,id``` will first sort by ```firstName``` and
      then by ```id```.
    - ```limit```: number of entities expected to get.
    - ```offset```: number of entities expected to skip before those that are included in response.<br>
      If both ```limit``` and ```offset``` are specified, response will include ```X-Total-Count``` header containing
      total number of entities.
- ### ```GET/:id```
  Response body will contain an entity with given ```id``` with all properties.<br>
  Returns ```404``` status if such entity is not found.
- ### ```PUT/:id```
  Updates entity with specified ID.<br>
  Request body should contain a valid entity JSON, ```id``` can be not specified here.<br>
  Response body will contain updated entity JSON if succeeded.<br>
  Returns ```404``` status if entity with such ```id``` is not found.<br>
  Returns ```422``` status if entity can't be updated because of DB schema limitations.
- ### ```POST```
  Creates new entity with properties as specified in request body.<br>
  Returns ```422``` status if entity can't be created because of DB integrity limitations.
- ### ```DELETE/:id```
  Deletes entity with specified ID.<br>
  Returns ```200``` status if succeeded.<br>
  Returns ```404``` status if entity with such ```id``` is not found.<br>
  Returns ```422``` status if entity can't be deleted because of DB schema limitations.
  