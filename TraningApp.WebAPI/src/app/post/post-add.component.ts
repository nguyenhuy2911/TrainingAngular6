import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { FormGroup, FormControl, Validators, FormBuilder, ValidationErrors } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http/src/response';

import { PostService } from '../services/post.service';

import { Post } from '../model/Post';
import { handleHttpResponseError } from '../authorize/handleHttpResponse';


@Component({
    selector: 'app-root',
    templateUrl: 'post-add.component.html'
})
export class PostAddComponent implements OnInit {
    errorMess: string;
    alertclass: string;
    form: FormGroup;
    post: Post = new Post();
   
    constructor(private postService: PostService, private router: Router) { }
    ngOnInit() {
        this.form = new FormGroup({
            title: new FormControl(this.post.Title, Validators.required),
            description: new FormControl(this.post.Description, Validators.required)
        });
    }
    onSubmit() {
        this.post = this.prepareSave();
        this.postService.createPost(this.post).subscribe(res => {
            if (res != null && res.isSuccess) {
                this.router.navigate(['/post]'])
            }
            else {
                this.errorMess = res.message
                this.alertclass = "alert alert-danger";
            }
        },
        (err: HttpErrorResponse) => {
            this.errorMess = handleHttpResponseError(err.status);
            this.alertclass = "alert alert-danger";
        }
        );
    }

    prepareSave(): Post {
        const formModel = this.form.value;
        const saveData: Post = {
            Id: 0 as number,
            Title: formModel.title as string,
            Description: formModel.description as string,
            CreateDate: new Date() as Date,
            Status: 0 as number
        };
        return saveData;
    }


}
