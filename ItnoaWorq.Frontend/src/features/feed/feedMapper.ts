import type { PostDto, Post } from "../../types/feed";

export const mapPostDtoToPost = (
  dto: PostDto,
  currentUserId?: string
): Post => {
  const currentReaction = dto.reactions.find(
    (r) => r.userId === currentUserId
  );

  return {
    id: dto.id,
    authorUserId: dto.authorUserId,
    content: dto.content,
    createdAt: dto.createdAt,

    comments: dto.comments,

    reactionCount: dto.reactions.length,
    currentUserReaction: currentReaction?.type,
  };
};