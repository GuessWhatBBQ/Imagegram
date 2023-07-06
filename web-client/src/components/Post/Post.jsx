import React, { useState, useEffect } from "react";
import { Galleria } from "primereact/galleria";
import Comments from "../Comments/Comments";

export default function Post({ images, caption, comments }) {
  const itemTemplate = (item) => {
    return (
      <img
        src={item.itemImageSrc}
        alt={item.alt}
        style={{
          width: "100%",
          height: "40vh",
          objectFit: "contain",
          display: "block",
        }}
      />
    );
  };

  const thumbnailTemplate = (item) => {
    return (
      <img
        src={item.thumbnailImageSrc}
        alt={item.alt}
        style={{ display: "block" }}
      />
    );
  };

  return (
    <div className="card">
      <Galleria
        value={images.map(({ id }) => ({
          itemImageSrc: `/asset/image/${id}`,
        }))}
        numVisible={5}
        circular
        style={{ maxWidth: "640px" }}
        showItemNavigators
        showItemNavigatorsOnHover
        showIndicators
        showThumbnails={false}
        item={itemTemplate}
        thumbnail={thumbnailTemplate}
      />
      <p>{caption}</p>
      <Comments comments={comments} />
    </div>
  );
}
