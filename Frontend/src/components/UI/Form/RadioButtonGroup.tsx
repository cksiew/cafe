import React from 'react';
import { Field } from 'react-final-form';
import { FormControl, FormLabel, RadioGroup, FormControlLabel, Radio } from '@mui/material';

interface RadioButtonGroupProps {
  name: string;
  label: string;
  options: { value: string; label: string }[];
}

const RadioButtonGroup: React.FC<RadioButtonGroupProps> = ({
  name,
  label,
  options,
}) => {

  const validate = (value: string) => {

    if (!value) {
      return `${label} is required.`;
    }
    return undefined;
  };

  return (
    <FormControl component="fieldset">
      <FormLabel id={`${name}-label`}>{label}</FormLabel>
      <Field name={name} validate={validate}>
        {({ input, meta }) => (
          <div>
            <RadioGroup {...input} aria-labelledby={`${name}-label`} row>
              {options.map((option) => (
                <FormControlLabel
                  key={option.value}
                  value={option.value}
                  control={<Radio />}
                  label={option.label}
                />
              ))}
            </RadioGroup>
            {meta.touched && meta.error && (
              <span style={{ color: 'red' }}>{meta.error}</span>
            )}
          </div>
        )}
      </Field>
    </FormControl>
  );
};

export default RadioButtonGroup;
