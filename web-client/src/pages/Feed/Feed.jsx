import React, { useState, useEffect } from "react";

import { usePostStore } from "../../store";
import Post from "../../components/Post/Post";

export default function Feed() {
  const { posts, fetch } = usePostStore(({ fetch, posts }) => ({
    fetch,
    posts,
  }));
  useEffect(() => {
    fetch({ after: "2021-07-02T21:17:33.352Z", size: 10, skip: 0 });
  }, []);

  const postsView = posts.map((post) => {
    const { caption, images, postId, comments } = post;
    return (
      <div className="card flex justify-content-center">
        <div className="card-container">
          <Post
            key={postId}
            caption={caption}
            images={images}
            comments={comments}
            postId={postId}
          />
        </div>
      </div>
    );
  });

  return (
    <>
      <div className="flex flex-column gap-3 my-3">{postsView}</div>
    </>
  );
}
