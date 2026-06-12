export enum ReactionType {
  Like = 0,
  Love = 1,
  Celebrate = 2,
}

export interface CommentDto {
  id: string;
  postId: string;
  authorUserId: string;
  content: string;
  createdAt: string;
}

export interface ReactionDto {
  id: string;
  postId: string;
  userId: string;
  type: ReactionType;
}

export interface PostDto {
  id: string;
  authorUserId: string;
  content: string;
  createdAt: string;
  comments: CommentDto[];
  reactions: ReactionDto[];
}