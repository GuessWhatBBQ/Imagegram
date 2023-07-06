import React from "react";
import { Menubar } from "primereact/menubar";
import { useNavigate, useLocation } from "react-router-dom";
import { useUserStore } from "../../store";
import { Button } from "primereact/button";
import NewPost from "../NewPost/NewPost";

export default function Navbar() {
  const navigate = useNavigate();
  const location = useLocation();

  const { session, logout } = useUserStore(({ session, logout }) => ({
    session,
    logout,
  }));

  const handleLogoutClick = () => {
    logout();
    navigate("/login");
  };
  const handleLoginClick = () => {
    navigate("/login");
  };
  const handleSignupClick = () => {
    navigate("/signup");
  };

  let items;
  let end;
  if (session.isLoggedIn) {
    items = [
      {
        label: "Feed",
        icon: "pi pi-fw pi-home",
        command: () => {
          navigate("/feed");
        },
      },
    ];
    end = (
      <div className="flex gap-3">
        <NewPost />
        <Button label="Logout" size="small" onClick={handleLogoutClick} />
      </div>
    );
  } else {
    if (location.pathname === "/login") {
      end = (
        <Button
          label="Signup"
          severity="success"
          size="small"
          onClick={handleSignupClick}
        />
      );
    } else {
      end = (
        <Button
          label="Login"
          severity="success"
          size="small"
          onClick={handleLoginClick}
        />
      );
    }
  }

  return (
    <div className="card">
      <Menubar model={items} end={end} />
    </div>
  );
}
