import { Injectable } from '@angular/core';
import { api } from './api.service';
import { PostDto } from '../../models/feed.model';

@Injectable({ providedIn: 'root' })
export class FeedService {

  async getFeed(): Promise<PostDto[]> {
    const res = await api.get<PostDto[]>('/Posts/feed');
    return res.data;
  }

  async createPost(content: string) {
    await api.post('/Posts', JSON.stringify(content), {
      headers: { 'Content-Type': 'application/json' },
    });
  }

  async react(postId: string, type: number) {
    await api.post(`/Posts/${postId}/reaction`, type, {
      headers: { 'Content-Type': 'application/json' },
    });
  }

  async comment(postId: string, content: string) {
    await api.post(`/Posts/${postId}/comment`, JSON.stringify(content), {
      headers: { 'Content-Type': 'application/json' },
    });
  }
}