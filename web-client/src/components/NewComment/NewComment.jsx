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
      <InputText
        value={comment}
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
    </div>
  );
}
