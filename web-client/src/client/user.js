import { base } from "./base";

export const signup = (userInfo) => {
  return base.post("/user", { ...userInfo });
};
