import api from "../../lib/axios";
import type { PostDto } from "../../types/feed";

export const getFeedApi = async (): Promise<PostDto[]> => {
  const res = await api.get<PostDto[]>("/Posts/feed");
  return res.data;
};

export const createPostApi = async (content: string): Promise<{ id: string }> => {
  const res = await api.post<{ id: string }>("/Posts", content, {
    headers: { "Content-Type": "application/json" },
  });
  return res.data;
};

export const reactToPostApi = async (
  postId: string,
  type: number
): Promise<void> => {
  await api.post(`/Posts/${postId}/reaction`, type, {
    headers: { "Content-Type": "application/json" },
  });
};

export const addCommentApi = async (
  postId: string,
  content: string
): Promise<void> => {
  await api.post(`/Posts/${postId}/comment`, content, {
    headers: { "Content-Type": "application/json" },
  });
};