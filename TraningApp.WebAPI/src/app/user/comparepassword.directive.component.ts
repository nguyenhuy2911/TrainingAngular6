import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, AbstractControl, ValidatorFn } from '@angular/forms';
import { debug } from 'util';
import { ValidationErrors } from '@angular/forms';


export function comparepassword(control: AbstractControl): { [key: string]: any } {
    let valid = false;
    if (control.root.get("passWord") != null) {
        valid = control.value == control.root.get("passWord").value;
    }
    return !valid ? { 'comparePassword': { 'message': 're-password not equal password' } } : null;
}

export function compareEqualWith(controllName): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } => {
        let valid = false;
        if (control.root.get(controllName) != null) {
            valid = control.value == control.root.get(controllName).value;
        }
        return !valid ? { 'comparePassword': { 'message': 're-password not equal password' } } : null;
    };
}



