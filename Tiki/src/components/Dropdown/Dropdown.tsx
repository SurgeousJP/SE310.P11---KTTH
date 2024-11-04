import { Select } from "flowbite-react";
import React from "react";

interface DropdownProps {
  title: string;
  name: string;
  items: { key: number; value: string }[];
  value: string;
  onChange: (e: any, key: number) => void;
}

const Dropdown: React.FC<DropdownProps> = ({
  title,
  items,
  value,
  name,
  onChange,
}) => {
  const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const value = e.target.value;
    const selectedOption = items.find((option) => option.value === value);
    const key = selectedOption ? selectedOption.key : null;
    onChange(e, key);
  };
  console.log("Current items: ",items);
  return (
    <div className="flex flex-col">
      <span className="text-sm font-medium leading-5">{title}</span>
      <Select
        name={name}
        className="self-strech w-full"
        required
        value={value}
        onChange={handleChange}
      >
        {items.map((item, index) => (
          <option key={item.key} value={item.value}>
            {item.value}
          </option>
        ))}
      </Select>
    </div>
  );
};

export default Dropdown;
