import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http/src/response';

import { PostService } from '../services/post.service';
import { handleHttpResponseError } from '../authorize/handleHttpResponse';
import { Post } from '../model/Post';

@Component({
    selector: 'app-root',
    templateUrl: 'post.component.html',
    styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
    errMessage: string = "";
    alertclass: string = "";
    posts: Post[];
    constructor(private postService: PostService) { }
    ngOnInit() {
        this.getPosts();
    }
    getPosts(): void {
        this.postService.getPosts().subscribe(
            res => {
                debugger;
                this.posts = res;
            },
            (err: HttpErrorResponse) => {
                this.errMessage = handleHttpResponseError(err.status);
                this.alertclass = "alert alert-danger";
            }
        );
    }
}
