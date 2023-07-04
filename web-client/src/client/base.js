import axios from "axios";
import { useUserStore } from "../store";

const base = axios.create({
  baseURL: "/",
  timeout: 5000,
  headers: {
    "Content-Type": "application/json",
  },
});

base.interceptors.request.use((request) => {
  const {
    session: { id: xSessionId },
  } = useUserStore.getState();
  if (xSessionId && xSessionId.length !== 0) {
    request.headers.set("X-Session-Id", xSessionId);
  }
  return request;
});

export { base };
