import React, { useState, ChangeEvent } from 'react';
import { Field } from 'react-final-form';
import classes from './FileInputField.module.css';

interface FileInputFieldProps {
  name: string;
  label: string;
  isEdit?: boolean;
  currentFileName?: string;
  maxSize?: number;
  onChangeFile?: (file: File) => void;
}

const FileInputField: React.FC<FileInputFieldProps> = ({
  name,
  label,
  isEdit = false,
  currentFileName,
  maxSize = 2 * 1024 * 1024,
  onChangeFile,
}) => {

  const [file, setFile] = useState<File | null>(null);
  const [error, setError] = useState<string | null>(null);


  // Handle file selection
  const handleFileChange = (event: ChangeEvent<HTMLInputElement>) => {
    const selectedFile = event.target.files ? event.target.files[0] : null;

    setError(null);


    if (selectedFile) {
      // Check file size
      if (selectedFile.size > maxSize) {
        setError(`File size exceeds the ${maxSize / (1024 * 1024)}MB limit.`);
        setFile(null); // Clear the file if size is invalid
      } else {
        setFile(selectedFile);

        if (onChangeFile) {
          onChangeFile(selectedFile);
        }
      }
    }
  };

  return (
    <Field name={name}>
      {() => (
        <div className={classes.inputField}>
          <label htmlFor={`${name}Input`} className={classes['file-label']}>
            {label}
          </label>
          {/* We omit the value prop because file inputs can't be controlled */}
          <input
            type="file"
            id={`${name}Input`}
            accept="image/*"
            onChange={handleFileChange}
            multiple={false} // Only allow one file at a time
          />

          {/* Display the selected file name */}
          {file && (
            <div className={classes.fileName}>
              Selected File: {file.name}
            </div>
          )}

          {/* Display current file name if in edit mode and no file selected */}
          {!file && isEdit && currentFileName && (
            <div className={classes.fileName}>Current File: {currentFileName.slice(currentFileName.lastIndexOf('/') + 1)}</div>
          )}

          {error && (
            <div>
              <span className={classes.errorMessage}>{error}</span>
            </div>
          )}
        </div>
      )}
    </Field>
  );
};

export default FileInputField;
