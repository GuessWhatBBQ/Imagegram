import "./App.css";

import { Routes, Route } from "react-router-dom";

import Login from "./pages/Login/Login";

const App = () => {
  return (
    <Routes>
      {/* <Route path="/feed"> */}
      {/*   <Route path="create" element={<CreateBook />}></Route> */}
      {/*   <Route path="" element={<ListBooks />}></Route> */}
      {/* </Route> */}
      <Route path="/login">
        <Route path="" element={<Login />}></Route>
      </Route>
    </Routes>
  );
};

export default App;
