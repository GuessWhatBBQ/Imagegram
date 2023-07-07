import React, { useState, useEffect } from "react";
import { Galleria } from "primereact/galleria";
import { Card } from "primereact/card";
import Comments from "../Comments/Comments";
import NewComment from "../NewComment/NewComment";

import "./Post.css";

export default function Post({ images, caption, comments, postId }) {
  const responsiveOptions = [
    {
      breakpoint: "991px",
      numVisible: 4,
    },
    {
      breakpoint: "767px",
      numVisible: 3,
    },
    {
      breakpoint: "575px",
      numVisible: 1,
    },
  ];

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

  const header = () => {
    return (
      <Galleria
        value={images.map(({ id }) => ({
          itemImageSrc: `/asset/image/${id}`,
        }))}
        numVisible={5}
        circular
        className=""
        style={{ maxWidth: "680px", minWidth: "680px" }}
        showItemNavigators
        showItemNavigatorsOnHover
        showIndicators
        showThumbnails={false}
        responsiveOptions={responsiveOptions}
        item={itemTemplate}
        thumbnail={thumbnailTemplate}
      />
    );
  };

  return (
    <div className="cardd">
      <div className="card flex flex-column justify-content-center gap-3">
        <Card subTitle={caption} header={header}></Card>
      </div>
      <Comments comments={comments} />
      <NewComment postId={postId} />
    </div>
  );
}
