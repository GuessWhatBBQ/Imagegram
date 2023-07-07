import React, { useState, useEffect } from "react";
import { InputText } from "primereact/inputtext";
import { usePostStore } from "../../store";

export default function NewComment({ postId }) {
  const { createComment } = usePostStore(({ createComment }) => ({
    createComment,
  }));

  const onCommentSuccess = () => {
    setComment("");
  };

  const [comment, setComment] = useState("");

  return (
    <div>
      <span className="p-float-label mt-3">
        <InputText
          id="comment"
          value={comment}
          style={{ width: "100%" }}
          onChange={(e) => setComment(e.target.value)}
          onKeyDown={(event) =>
            event.key === "Enter"
              ? createComment(
                  { content: comment, postId: postId },
                  onCommentSuccess
                )
              : null
          }
        />
        <label htmlFor="comment">Let your friend know what you think</label>
      </span>
    </div>
  );
}
