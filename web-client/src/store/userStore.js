import { create } from "zustand";
import { persist, createJSONStorage } from "zustand/middleware";
import { authClient, userClient } from "../client";
import { AxiosError } from "axios";

const persistConfig = {
  name: "user-storage",
  storage: createJSONStorage(() => localStorage),
};

const loginActionBuilder = (set) => async (credentials, onSuccess) => {
  try {
    const xSessionId = await authClient.login(credentials).then((response) => {
      return response.headers.get("X-Session-Id");
    });
    set(() => ({ session: { id: xSessionId, isLoggedIn: true } }));
    onSuccess();
  } catch (err) {}
};

const signupActionBuilder = (set) => async (userInfo, onSuccess) => {
  try {
    await userClient.signup(userInfo);
    onSuccess();
  } catch (err) {
    const { response } = err;
    if (
      err instanceof AxiosError &&
      response &&
      response.status === 403 &&
      response.data.title === "Username must be unique"
    ) {
      set({ error: "Username has alredy been taken by another user" }, false);
    }
  }
};

const useUserStore = create(
  persist(
    (set) => ({
      session: { id: null, isLoggedIn: false },
      error: null,
      logout: () => set({ session: { id: null, isLoggedIn: false } }, false),
      login: loginActionBuilder(set),
      signup: signupActionBuilder(set),
    }),
    persistConfig
  )
);

export default useUserStore;
