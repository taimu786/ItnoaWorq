import { useAppDispatch } from "../../hooks/redux";
import type { Post, ReactionType } from "../../types/feed";
import { reactToPost, addComment } from "../../features/feed/feedSlice";
import { useState } from "react";

interface Props {
  post: Post;
}

export default function PostCard({ post }: Props) {
  const dispatch = useAppDispatch();
  const [comment, setComment] = useState("");
  const [showComments, setShowComments] = useState(false);

  return (
    <div className="bg-white p-4 rounded-lg shadow mb-4">

      {/* CONTENT */}
      <p>{post.content}</p>

      {/* REACTIONS */}
      <div className="text-sm text-gray-500 mt-2">
        {post.reactionCount} reactions
      </div>

      {/* ACTIONS */}
      <div className="flex gap-4 mt-3">
        <button
          onClick={() =>
            dispatch(
              reactToPost({ postId: post.id, type: 0 })
            )
          }
        >
          👍 Like
        </button>

        <button onClick={() => setShowComments(!showComments)}>
          💬 Comment
        </button>
      </div>

      {/* COMMENTS */}
      {showComments && (
        <div className="mt-3">
          {post.comments.map((c) => (
            <div key={c.id}>
              <b>{c.authorUserId}</b>: {c.content}
            </div>
          ))}

          <div className="flex gap-2 mt-2">
            <input
              value={comment}
              onChange={(e) => setComment(e.target.value)}
              className="border px-2 py-1"
            />
            <button
              onClick={() => {
                dispatch(addComment({ postId: post.id, content: comment }));
                setComment("");
              }}
            >
              Post
            </button>
          </div>
        </div>
      )}
    </div>
  );
}