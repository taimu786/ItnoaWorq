// src/features/feed/feedSlice.ts

import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import {
  getFeedApi,
  createPostApi,
  reactToPostApi,
  addCommentApi,
} from "./feedService";
import type { Post } from "../../types/feed";
import { mapPostDtoToPost } from "./feedMapper";
import type { RootState } from "../../app/store";

interface FeedState {
  posts: Post[];
  loading: boolean;
}

const initialState: FeedState = {
  posts: [],
  loading: false,
};

// 🔹 FETCH FEED
export const fetchFeed = createAsyncThunk(
  "feed/fetchFeed",
  async (_, { getState }) => {
    const data = await getFeedApi();
    const state = getState() as RootState;

    const userId = state.auth.user?.id;

    return data.map((dto) => mapPostDtoToPost(dto, userId));
  }
);

// 🔹 CREATE POST
export const createPost = createAsyncThunk(
  "feed/createPost",
  async (content: string, { dispatch }) => {
    await createPostApi(content);
    dispatch(fetchFeed());
  }
);

// 🔹 REACT
export const reactToPost = createAsyncThunk(
  "feed/reactToPost",
  async (
    { postId, type }: { postId: string; type: number },
    { dispatch }
  ) => {
    await reactToPostApi(postId, type);
    dispatch(fetchFeed());
  }
);

// 🔹 COMMENT
export const addComment = createAsyncThunk(
  "feed/addComment",
  async (
    { postId, content }: { postId: string; content: string },
    { dispatch }
  ) => {
    await addCommentApi(postId, content);
    dispatch(fetchFeed());
  }
);

const feedSlice = createSlice({
  name: "feed",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchFeed.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchFeed.fulfilled, (state, action) => {
        state.loading = false;
        state.posts = action.payload;
      })
      .addCase(fetchFeed.rejected, (state) => {
        state.loading = false;
      });
  },
});

export default feedSlice.reducer;