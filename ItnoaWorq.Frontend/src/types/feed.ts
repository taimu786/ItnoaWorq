// src/types/feed.ts

export type ReactionType = 0 | 1 | 2;

// 🔹 Backend DTOs

export interface ReactionDto {
  id: string;
  postId: string;
  userId: string;
  type: ReactionType;
}

export interface CommentDto {
  id: string;
  postId: string;
  authorUserId: string;
  content: string;
  createdAt: string;
}

export interface PostDto {
  id: string;
  authorUserId: string;
  content: string;
  createdAt: string;
  comments: CommentDto[];
  reactions: ReactionDto[];
}

// 🔹 Frontend Model (UI optimized)

export interface Post {
  id: string;
  authorUserId: string;
  content: string;
  createdAt: string;

  comments: CommentDto[];

  reactionCount: number;
  currentUserReaction?: ReactionType;
}