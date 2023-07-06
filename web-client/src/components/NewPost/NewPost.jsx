import React, { useState, useRef } from "react";
import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";
import { FileUpload } from "primereact/fileupload";
import { InputTextarea } from "primereact/inputtextarea";
import { Toast } from "primereact/toast";
import { Tooltip } from "primereact/tooltip";
import { Tag } from "primereact/tag";
import { XHRClientInterceptor } from "../../client/base";

export default function NewPost() {
  const [visible, setVisible] = useState(false);
  const [caption, setCaption] = useState("");
  const toast = useRef(null);
  const fileUploadRef = useRef(null);

  const onTemplateUpload = (e) => {
    toast.current.show({
      severity: "info",
      summary: "Success",
      detail: "Your post just got published!",
    });
    setCaption("");
    setVisible(false);
  };

  const headerTemplate = (options) => {
    const { className, chooseButton, uploadButton, cancelButton } = options;
    return (
      <div
        className={className}
        style={{
          backgroundColor: "transparent",
          display: "flex",
          alignItems: "center",
        }}
      >
        {chooseButton}
        {uploadButton}
        {cancelButton}
      </div>
    );
  };

  const itemTemplate = (file, props) => {
    return (
      <div className="flex align-items-center flex-wrap">
        <div className="flex align-items-center" style={{ width: "40%" }}>
          <img
            alt={file.name}
            role="presentation"
            src={file.objectURL}
            width={100}
          />
        </div>
        <Button
          type="button"
          icon="pi pi-times"
          className="p-button-outlined p-button-rounded p-button-danger ml-auto"
          onClick={() => props.onRemove()}
        />
      </div>
    );
  };

  const emptyTemplate = () => {
    return (
      <div className="flex align-items-center flex-column">
        <i
          className="pi pi-image mt-3 p-5"
          style={{
            fontSize: "5em",
            borderRadius: "50%",
            backgroundColor: "var(--surface-b)",
            color: "var(--surface-d)",
          }}
        ></i>
        <span
          style={{ fontSize: "1.2em", color: "var(--text-color-secondary)" }}
          className="my-5"
        >
          Drag and Drop Image Here
        </span>
      </div>
    );
  };

  const chooseOptions = {
    icon: "pi pi-fw pi-images",
    iconOnly: true,
    className: "custom-choose-btn p-button-rounded p-button-outlined",
  };
  const uploadOptions = {
    icon: "pi pi-fw pi-cloud-upload",
    iconOnly: true,
    className:
      "custom-upload-btn p-button-success p-button-rounded p-button-outlined",
  };
  const cancelOptions = {
    icon: "pi pi-fw pi-times",
    iconOnly: true,
    className:
      "custom-cancel-btn p-button-danger p-button-rounded p-button-outlined",
  };

  const onBeforeUpload = (
    /** @type import("primereact/fileupload").FileUploadBeforeUploadEvent */ event
  ) => {
    event.formData.append("caption", caption);
  };

  const onBeforeSend = (
    /** @type import("primereact/fileupload").FileUploadBeforeSendEvent */ event
  ) => {
    XHRClientInterceptor(event.xhr);
  };

  return (
    <React.Fragment>
      <Toast ref={toast}></Toast>

      <Tooltip target=".custom-choose-btn" content="Choose" position="bottom" />
      <Tooltip target=".custom-upload-btn" content="Upload" position="bottom" />
      <Tooltip target=".custom-cancel-btn" content="Clear" position="bottom" />
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
              autoResize
              onChange={(e) => setCaption(e.target.value)}
              rows={20}
              cols={60}
            />
            <FileUpload
              ref={fileUploadRef}
              name="images"
              url="/post"
              multiple
              accept="image/*"
              onUpload={onTemplateUpload}
              onBeforeUpload={onBeforeUpload}
              onBeforeSend={onBeforeSend}
              headerTemplate={headerTemplate}
              itemTemplate={itemTemplate}
              emptyTemplate={emptyTemplate}
              chooseOptions={chooseOptions}
              uploadOptions={uploadOptions}
              cancelOptions={cancelOptions}
            />
          </div>
        </Dialog>
      </div>
    </React.Fragment>
  );
}
