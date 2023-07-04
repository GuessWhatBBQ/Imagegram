import { create } from "zustand";
import { persist, createJSONStorage } from "zustand/middleware";
import { authClient } from "../client";

const persistConfig = {
  name: "user-storage",
  storage: createJSONStorage(() => localStorage),
};

const loginActionBuilder = (set) => async (credentials) => {
  const xSessionId = await authClient.login(credentials).then((response) => {
    return response.headers.get("X-Session-Id");
  });
  set(() => ({ session: { id: xSessionId, isLoggedIn: true } }));
};

const useUserStore = create(
  persist(
    (set) => ({
      session: { id: null, isLoggedIn: false },
      login: loginActionBuilder(set),
    }),
    persistConfig
  )
);

export default useUserStore;
