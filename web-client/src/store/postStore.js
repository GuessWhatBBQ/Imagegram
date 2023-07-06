import { create } from "zustand";
import { persist, createJSONStorage } from "zustand/middleware";
import { postClient } from "../client";
import { commentClient } from "../client";

const persistConfig = {
  name: "post-storage",
  storage: createJSONStorage(() => localStorage),
};

const fetchActionBuilder = (set) => async (criteria) => {
  const posts = await postClient
    .fetchFeed(criteria)
    .then((response) => response.data);
  set(() => ({ posts }));
};

const createCommentActionBuilder = (set) => async (comment, onSuccess) => {
  comment = await commentClient
    .createComment(comment)
    .then((response) => response.data);
  set(({ posts }) => {
    return {
      posts: posts.map((post) =>
        post.postId === comment.postId
          ? { ...post, comments: [...post.comments, comment] }
          : post
      ),
    };
  });
  onSuccess();
};

const usePostStore = create(
  persist(
    (set) => ({
      posts: [],
      fetch: fetchActionBuilder(set),
      createComment: createCommentActionBuilder(set),
    }),
    persistConfig
  )
);

export default usePostStore;
