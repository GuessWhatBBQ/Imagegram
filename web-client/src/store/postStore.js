import { create } from "zustand";
import { persist, createJSONStorage } from "zustand/middleware";
import { postClient } from "../client";

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

const usePostStore = create(
  persist(
    (set) => ({
      posts: [],
      fetch: fetchActionBuilder(set),
    }),
    persistConfig
  )
);

export default usePostStore;
