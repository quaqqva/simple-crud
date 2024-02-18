import { HttpHeaders, HttpInterceptorFn } from '@angular/common/http';
import { isDevMode } from '@angular/core';

const urlInterceptor: HttpInterceptorFn = (req, next) => {
  const url = isDevMode() ? 'http://localhost:5134' : '/api';
  const newReq = req.clone({
    url: `${url}/${req.url}`,
    headers: new HttpHeaders({
      'X-From-Frontend': 'true',
    }),
  });
  return next(newReq);
};
export default urlInterceptor;
