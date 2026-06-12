import api from "@/lib/axios";
import { Post } from "@/types/post";

export type FeedResponse = {
  items: Post[];
  page: number;
  pageSize: number;
  totalCount: number;
};

export const PostsService = {
  getFeed: async (page = 1, pageSize = 10): Promise<FeedResponse> => {
    const res = await api.get<FeedResponse>(
      `/Posts/feed?page=${page}&pageSize=${pageSize}`
    );
    return res.data;
  },
};
