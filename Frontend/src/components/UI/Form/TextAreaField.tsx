import React from 'react';
import { Field, FieldRenderProps } from 'react-final-form';
import classes from './TextAreaField.module.css';

interface TextAreaFieldProps {
  name: string;
  label: string;
  placeholder: string;
  cols?: number;
  rows?: number;
  maxLength: number;
  required?: boolean;
}

const TextAreaField: React.FC<TextAreaFieldProps> = ({
  name,
  label,
  placeholder,
  cols = 50,
  rows = 5,
  maxLength = 256,
  required
}) => {

  const validate = (value: string) => {
    if (required && !value) {
      return `${label} is required.`;
    }

    if (maxLength && value.length > maxLength) {
      return `Maximum length is ${maxLength} characters.`;
    }

    return undefined;
  }

  return (
    <Field name={name} validate={validate}>
      {({ input, meta }: FieldRenderProps<string>) => (
        <div className={classes.inputField}>
          <label>{label}</label>
          <textarea
            {...input}
            placeholder={placeholder}
            cols={cols}
            rows={rows}
          />
          {meta.error && meta.touched && (
            <span className={classes.errorMessage}>{meta.error}</span>
          )}
        </div>
      )}
    </Field>
  );
};

export default TextAreaField;
