export type PostAuthor = {
  id: string;
  fullName: string;
  headline?: string;
  avatarUrl?: string;
};

export type Post = {
  id: string;
  content: string;
  createdAt: string;
  author: PostAuthor;
  reactionsCount: number;
  commentsCount: number;
  isReactedByMe?: boolean;
};
