import React from "react";

import "./Comment.css";

export default function Comments({ comments }) {
  const commentsView = comments
    .sort(
      (first, second) =>
        new Date(first.creationDate).getTime() -
        new Date(second.creationDate).getTime()
    )
    .map(({ content, id }) => {
      return (
        <div className="carddd p-3">
          <p className="comment-text" key={id}>
            {content}
          </p>
        </div>
      );
    });

  return <div className="flex flex-column card gap-3 my-3">{commentsView}</div>;
}
