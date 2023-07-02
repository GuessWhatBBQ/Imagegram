import React from "react";
import { Menubar } from "primereact/menubar";
import { useNavigate } from "react-router-dom";

export default function Navbar() {
  const navigate = useNavigate();
  const items = [
    {
      label: "Home",
      icon: "pi pi-fw pi-home",
      command: () => {
        navigate("/login");
      },
    },
  ];

  return (
    <div className="card">
      <Menubar model={items} />
    </div>
  );
}
