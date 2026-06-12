import { useState } from "react";
import toast from "react-hot-toast";
import { createPostApi } from "../../features/feed/feedService";

export default function CreatePostModal({ isOpen, onClose, onSuccess }: any) {
  const [content, setContent] = useState("");
  const [loading, setLoading] = useState(false);

  if (!isOpen) return null;

  const handlePost = async () => {
    if (!content.trim()) {
      toast.error("Post cannot be empty");
      return;
    }

    try {
      setLoading(true);
      await createPostApi(content);

      toast.success("Post created 🎉");
      setContent("");
      onClose();
      onSuccess(); // refresh feed
    } catch {
      toast.error("Failed to post");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="fixed inset-0 bg-black/40 flex items-center justify-center z-50">

      <div className="bg-white w-full max-w-lg rounded-xl shadow-lg p-4">

        {/* HEADER */}
        <div className="flex justify-between items-center mb-4">
          <h2 className="text-lg font-semibold">Create a post</h2>
          <button onClick={onClose}>✖</button>
        </div>

        {/* USER INFO */}
        <div className="flex items-center gap-3 mb-3">
          <div className="w-10 h-10 bg-gray-300 rounded-full" />
          <div>
            <p className="font-semibold">You</p>
            <p className="text-xs text-gray-500">Post to anyone</p>
          </div>
        </div>

        {/* TEXTAREA */}
        <textarea
          value={content}
          onChange={(e) => setContent(e.target.value)}
          placeholder="What do you want to talk about?"
          className="w-full min-h-[120px] p-2 outline-none resize-none"
        />

        {/* ACTION BAR (LinkedIn style placeholder) */}
        <div className="flex justify-between items-center mt-4">

          <div className="flex gap-4 text-gray-500">
            <span>📷</span>
            <span>🎥</span>
            <span>📄</span>
          </div>

          <button
            onClick={handlePost}
            disabled={loading}
            className="bg-indigo-600 text-white px-4 py-2 rounded-full hover:bg-indigo-700 disabled:opacity-50"
          >
            {loading ? "Posting..." : "Post"}
          </button>

        </div>

      </div>
    </div>
  );
}