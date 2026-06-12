"use client";

import { useEffect } from "react";
import { useFeedStore } from "@/store/feed/feed.store";
import { Card, CardContent } from "@/components/ui/card";

export default function FeedPage() {
  const { posts, loadFeed, loading } = useFeedStore();

  useEffect(() => {
    loadFeed();
  }, []);

  return (
    <div className="max-w-2xl mx-auto space-y-4">
      <h1 className="text-2xl font-semibold mb-4">Feed</h1>

      {posts.map((post) => (
        <Card key={post.id}>
          <CardContent className="p-4 space-y-2">
            <div className="font-semibold">{post.author.fullName}</div>
            <div className="text-sm text-muted-foreground">
              {new Date(post.createdAt).toLocaleString()}
            </div>
            <p className="mt-2">{post.content}</p>
            <div className="text-sm text-muted-foreground mt-2">
              👍 {post.reactionsCount} · 💬 {post.commentsCount}
            </div>
          </CardContent>
        </Card>
      ))}

      {loading && <div className="text-center">Loading…</div>}
    </div>
  );
}
