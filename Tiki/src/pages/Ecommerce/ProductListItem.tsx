import { KurumiList } from "@/assets/mockdata"
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

interface ProductListItemProps{
  item: BookGeneralInfoDTO,
  id: number
}

const ProductListItem:React.FC<ProductListItemProps> = (props) => {

  const navigate = useNavigate();

  const handleEditBook = (e) => {
    navigate(`/edit-product/${props.id}`);
  }

  const handleDeleteBook = (e) => {
    
  }

  return (
    <div className="flex flex-row w-full justify-between items-center p-4 cursor-pointer">
      <div className="flex flex-row gap-4 items-center">
        <img
          src={props.item.imageUrl}
          alt="product image"
          width={64}
          height={64}
          className="object-cover"
        />
        <span className="text-lg w-60">{props.item.title}</span>
      </div>
      <span className="text-md">{props.item.price?.toFixed(0)}$</span>
      <span className="text-md">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{props.item.availability}</span>
      <div className="flex flex-row gap-4">
        <button onClick={handleEditBook} className="border-1 rounded-md p-2 px-4 hover:bg-green-200">
          Edit
        </button>
        <button className="border-1 rounded-md p-2 px-4 hover:bg-red-500">Delete</button>
      </div>
    </div>
  );
};

export default ProductListItem;
