import { useEffect, useState } from "react";
import MainLayout from "../../components/Layouts/MainLayout";
import { useDispatch, useSelector } from "react-redux";
import { fetchFeed } from "../../features/feed/feedSlice";
import CreatePostModal from "../../components/feed/CreatePostModal";
import type { AppDispatch } from "../../app/store";
import PostCard from "../../components/feed/PostCard";
import type { RootState } from "../../app/store";

export default function FeedPage() {
  const [open, setOpen] = useState(false);
  const dispatch = useDispatch<AppDispatch>();
  const posts = useSelector((state: RootState) => state.feed.posts);

  const loadFeed = async () => {
    dispatch(fetchFeed());
  };

  useEffect(() => {
    loadFeed();
  }, []);

  return (
    <MainLayout layout="feed">

      {/* CREATE POST BOX */}
      <div className="bg-white p-4 rounded-lg shadow mb-4">
        <div
          onClick={() => setOpen(true)}
          className="border rounded-full px-4 py-2 cursor-pointer text-gray-500 hover:bg-gray-100"
        >
          Start a post...
        </div>
      </div>

      {/* POSTS */}
      {posts.map((post) => (
        <PostCard post={post}></PostCard>
      ))}

      {/* MODAL */}
      <CreatePostModal
        isOpen={open}
        onClose={() => setOpen(false)}
        onSuccess={loadFeed}
      />

    </MainLayout>
  );
}