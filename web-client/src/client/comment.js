import { base } from "./base";

export const createComment = (comment) => {
  return base.post("/comment", comment);
};
