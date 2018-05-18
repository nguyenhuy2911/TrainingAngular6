import { Component, OnInit } from '@angular/core';

export function handleHttpResponseError(httpStatus: number) {
    if (httpStatus == 400) {
        return "Request is not good";
    }
    if (httpStatus == 403) {
        return "You don't have permission";
    }
    
}


