import { bookApi } from "@/apis/book.api";
import { KurumiList } from "@/assets/mockdata";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

interface ProductListItemProps {
  item: BookGeneralInfoDTO;
  id: number;
  pageIndex: number;
  pageSize: number;
}

const ProductListItem: React.FC<ProductListItemProps> = (props) => {
  const navigate = useNavigate();

  const handleEditBook = (e) => {
    navigate(`/edit-product/${props.id}`);
  };

  const handleDeleteBook = (e) => {
    console.log("Delete book")
    deleteBookMutation.mutate(props.id);
  };

  const queryClient = useQueryClient();

  const deleteBookMutation = useMutation({
    mutationKey: ["book", "delete", props.id],
    mutationFn: async (id: number) => {
      console.log("Id for delteion", id);
      if (id === null || id === undefined) {
        toast.error("Book id is not valid");
        return;
      }

      console.log(`Prepare to delete book with ${id}`);
      const result = await bookApi.deleteBook(id);
      if (result.status !== 200) {
        toast.error(result.data);
        return;
      }
    },
    onSuccess: () => {
      toast.success(
        `The book with bookId: ${props.id} has successfully deleted`
      );
      queryClient.invalidateQueries({ queryKey: ["books", props.pageIndex, props.pageSize] });
      console.log("Began navigating back to book grid page");
      navigate(`/admin-list`);
    },
  });

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
      <span className="text-md">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        {props.item.availability}
      </span>
      <div className="flex flex-row gap-4">
        <button
          onClick={handleEditBook}
          className="border-1 rounded-md p-2 px-4 hover:bg-green-200"
        >
          Edit
        </button>
        <button
          onClick={handleDeleteBook}
          className="border-1 rounded-md p-2 px-4 hover:bg-red-500"
        >
          Delete
        </button>
      </div>
    </div>
  );
};

export default ProductListItem;
