import React from 'react';
import { Field, FieldRenderProps } from 'react-final-form';
import classes from './TextField.module.css'

interface TextFieldProps {
  name: string;
  label: string;
  minLength?: number;
  maxLength?: number;
  placeholder?: string;
  required?: boolean;
  type?: 'text' | 'email' | 'phoneNo';
}

const TextField: React.FC<TextFieldProps> = ({
  name,
  label,
  minLength,
  maxLength,
  placeholder,
  required,
  type = 'text',
}) => {
  const validate = (value: string) => {

    if (required && value.trim() === '') {
      return `${label} is required.`;
    }

    if (type === 'phoneNo' && value && !/[89][0-9]{7}$/i.test(value)) {
      return "Invalid phone number.";
    }
    if (type === 'email' && value && !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i.test(value)) {
      return 'Invalid email address.';
    }

    if (minLength && value.length < minLength) {
      return `Minimum length is ${minLength} characters.`;
    }
    if (maxLength && value.length > maxLength) {
      return `Maximum length is ${maxLength} characters.`;
    }
    return undefined;
  };

  return (
    <Field
      name={name}
      validate={validate}
    >
      {({ input, meta }: FieldRenderProps<string>) => (
        <div className={classes.inputField}>
          <label>{label}</label>
          <input
            {...input}
            type={type}
            placeholder={placeholder || label}
          />
          {meta.error && meta.touched && (
            <span className={classes.errorMessage}>{meta.error}</span>
          )}
        </div>
      )}
    </Field>
  );
};

export default TextField;
