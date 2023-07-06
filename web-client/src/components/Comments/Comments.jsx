import React from "react";

export default function Comments({ comments }) {
  const commentsView = comments
    .sort(
      (first, second) =>
        new Date(first.creationDate).getTime() -
        new Date(second.creationDate).getTime()
    )
    .map(({ content, id }) => {
      return <p key={id}>{content}</p>;
    });

  return <div className="card">{commentsView}</div>;
}
