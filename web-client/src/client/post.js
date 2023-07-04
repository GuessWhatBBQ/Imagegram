import { base } from "./base";

export const fetchFeed = (criteria) => {
  return base.get("/feed", { params: criteria });
};
