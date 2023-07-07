import "./App.css";

import { Routes, Route } from "react-router-dom";

import Login from "./pages/Login/Login";
import Feed from "./pages/Feed/Feed";
import Signup from "./pages/Signup/Signup";
import { useUserStore } from "./store";

const App = () => {
  const { session } = useUserStore(({ session }) => ({
    session,
  }));

  return (
    <Routes>
      <Route index element={session.isLoggedIn ? <Feed /> : <Login />} />
      <Route path="/login">
        <Route path="" element={<Login />}></Route>
      </Route>
      <Route path="/signup">
        <Route path="" element={<Signup />}></Route>
      </Route>
      <Route path="/feed">
        <Route path="" element={<Feed />}></Route>
      </Route>
    </Routes>
  );
};

export default App;
