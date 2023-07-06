import React, { useState } from "react";
import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";
import { FileUpload } from "primereact/fileupload";
import { InputTextarea } from "primereact/inputtextarea";

export default function NewPost() {
  const [visible, setVisible] = useState(false);
  const [caption, setCaption] = useState("");

  return (
    <div className="card flex justify-content-center">
      <Button
        label="Create a Post"
        icon="pi pi-pencil"
        size="small"
        onClick={() => setVisible(true)}
      />
      <Dialog
        header="New Post"
        visible={visible}
        style={{ width: "50vw" }}
        onHide={() => setVisible(false)}
      >
        <div className="card flex gap-5">
          <InputTextarea
            value={caption}
            onChange={(e) => setCaption(e.target.value)}
            rows={5}
            cols={30}
          />
          <FileUpload
            name="images[]"
            url={"/api/upload"}
            multiple
            accept="image/*"
            maxFileSize={1000000}
            emptyTemplate={
              <p className="m-0">Drag and drop files to here to upload.</p>
            }
          />{" "}
        </div>
      </Dialog>
    </div>
  );
}
