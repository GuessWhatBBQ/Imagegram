import { base } from "./base";

export const login = ({ username, password }) => {
  return base.post("/auth", {
    username,
    password,
  });
};
