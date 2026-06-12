import { Component, OnInit } from '@angular/core';
import { FeedService } from '../../../core/services/feed.service';
import { PostDto } from '../../../models/feed.model';

@Component({
  selector: 'app-feed',
  standalone: false,
  templateUrl: './feed.html',
})
export class Feed implements OnInit {

  posts: PostDto[] = [];
  content = '';
  loading = false;

  constructor(private feedService: FeedService) {}

  async ngOnInit() {
    await this.loadFeed();
  }

  async loadFeed() {
    this.posts = await this.feedService.getFeed();
  }

  async createPost() {
    if (!this.content) return;

    await this.feedService.createPost(this.content);
    this.content = '';
    await this.loadFeed();
  }

  async react(postId: string, type: number) {
    await this.feedService.react(postId, type);
    await this.loadFeed();
  }

  async comment(postId: string, input: HTMLInputElement) {
    if (!input.value) return;

    await this.feedService.comment(postId, input.value);
    input.value = '';
    await this.loadFeed();
  }
}