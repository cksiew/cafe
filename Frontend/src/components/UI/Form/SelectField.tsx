import React from 'react';
import { Field, FieldRenderProps } from 'react-final-form';
import classes from './SelectField.module.css';

interface SelectFieldProps {
  name: string;
  label: string;
  options: { value: string | number; label: string }[];
}

const SelectField: React.FC<SelectFieldProps> = ({
  name,
  label,
  options,
}) => {
  const validate = (value: string) => {

    if (!value || value === "0") {
      return `${label} is required.`;
    }
    return undefined;
  };

  return (
    <Field name={name} validate={validate}>
      {({ input, meta }: FieldRenderProps<string | number>) => (
        <div className={classes.inputField}>
          <label>{label}</label>
          <select {...input}>
            <option value="0" key="0">-- Select {label} --</option>
            {options.map((option) => (
              <option value={option.value} key={option.value}>
                {option.label}
              </option>
            ))}
          </select>
          {meta.error && meta.touched && (
            <span className={classes.errorMessage}>{meta.error}</span>
          )}
        </div>
      )}
    </Field>
  );
};

export default SelectField;
