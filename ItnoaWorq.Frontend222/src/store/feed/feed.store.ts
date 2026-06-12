import { create } from "zustand";
import { Post } from "@/types/post";
import { PostsService } from "@/services/posts/posts.service";

type FeedState = {
  posts: Post[];
  loading: boolean;
  page: number;
  hasMore: boolean;
  loadFeed: () => Promise<void>;
};

export const useFeedStore = create<FeedState>((set, get) => ({
  posts: [],
  loading: false,
  page: 1,
  hasMore: true,

  loadFeed: async () => {
    if (get().loading || !get().hasMore) return;

    set({ loading: true });
    try {
      const { items, totalCount, pageSize } =
        await PostsService.getFeed(get().page);

      const newPosts = [...get().posts, ...items];

      set({
        posts: newPosts,
        page: get().page + 1,
        hasMore: newPosts.length < totalCount,
      });
    } finally {
      set({ loading: false });
    }
  },
}));
