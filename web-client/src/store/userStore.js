import { create } from "zustand";
import { persist, createJSONStorage } from "zustand/middleware";
import { authClient, userClient } from "../client";

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

const signupActionBuilder = (get) => async (userInfo) => {
  await userClient.signup(userInfo);
  get().login(userInfo);
};

const useUserStore = create(
  persist(
    (set, get) => ({
      session: { id: null, isLoggedIn: false },
      logout: () => set({ session: { id: null, isLoggedIn: false } }, false),
      login: loginActionBuilder(set),
      signup: signupActionBuilder(get),
    }),
    persistConfig
  )
);

export default useUserStore;
