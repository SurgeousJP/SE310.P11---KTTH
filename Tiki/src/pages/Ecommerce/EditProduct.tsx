import { bookApi } from "@/apis/book.api";
import Input from "@/components/Input/Input";
import { BookDetailDTO } from "@/types/DTOs/BookCatalog/BookDetailDTO.type";
import { CreateBookDTO } from "@/types/DTOs/BookCatalog/CreateBookDTO.type";
import { useMutation, useQuery } from "@tanstack/react-query";
import { Dropdown, Select } from "flowbite-react";
import React, { useEffect, useState } from "react";
import { BiSolidMessageRoundedCheck } from "react-icons/bi";
import { NavigationType, useNavigate, useParams } from "react-router-dom";
import { toast } from "react-toastify";

const EditProduct = () => {

  const { id } = useParams();
  const { data: bookData, isLoading: isLoadingBookData } = useQuery(
    {
      queryKey: ['book', id],
      queryFn: () => {
        return bookApi.getBook(id);
      }
    }
  ); 

  const [book, setBook] = useState({
    id: id,
    languageCode: "",
    averageRating: 0,
    description: "",
    numPages: 0,
    publicationDay: 1,
    publicationMonth: 11,
    publicationYear: 2024,
    isbn13: "",
    imageUrl: "",
    ratingsCount: 0,
    title: "",
    titleWithoutSeries: "",
    price: 0,
    availability: 0,
    dimensions: "0 x 0 x 0",
    discountPercentage: 0,
    itemWeight: 0,
    authorName: "",
    formatId: 1,
    publisherId: 1,
    genre_id: 1,
  });

  useEffect(() => {
    if (!isLoadingBookData && bookData){
      const data = bookData.data;
      setBook({
        id: id,
        languageCode: data.languageCode ?? "",
        averageRating: data.averageRating ?? 0,
        description: data.description ?? "",
        numPages: data.numPages ?? 0,
        publicationDay: data.publicationDay ?? 1,
        publicationMonth: data.publicationMonth ?? 11,
        publicationYear: data.publicationYear ?? 2024,
        isbn13: "",
        imageUrl: data.imageUrl ?? "",
        ratingsCount: data.ratingsCount ?? 0,
        title: data.title ?? "",
        titleWithoutSeries: data.titleWithoutSeries ?? "",
        price: data.price ?? 0,
        availability: data.availability ?? 0,
        dimensions: data.dimensions ?? "2 x 2 x 2 inches",
        discountPercentage: data.discountPercentage ?? 0,
        itemWeight: data.itemWeight ?? 10,
        authorName: data.authorName ?? "",
        formatId: data.formatId ?? 1,
        publisherId: data.publisherId ?? 1,
        genre_id: data.genre_id ?? 1,
      })
    }
  }, [bookData, isLoadingBookData])

  const [currentGenreId, setCurrentGenreId] = useState(38);
  const onDropdownChange = (e) => {
    console.log("Current genre id: ", parseInt(e.target.value));
    setCurrentGenreId(parseInt(e.target.value));
  };

  useEffect(() => {
    setBook({ ...book, ["genre_id"]: currentGenreId });
  }, [currentGenreId]);

  useEffect(() => {
    console.log(book);
  }, [book]);

  const onInputChange = (e) => {
    const { name, value } = e.target;
    // Check if the value is a number and convert if necessary
    const parsedValue =
      isNaN(value) || value.trim() === "" ? value : parseFloat(value);
    console.log(parsedValue);
    setBook({ ...book, [name]: value });
  };

  const { data: categories, isLoading: isLoadingCategory } = useQuery({
    queryKey: ["categories"],
    queryFn: async () => {
      return (await bookApi.getGenresByPage(0, 100)).data;
    },
  });

  const navigate = useNavigate();

  const updateProductMutation = useMutation({
    mutationKey: ['update-product', book],
    mutationFn: async(book: BookDetailDTO) => {
      console.log("Update book prepare...: " + book)
      const result = await bookApi.updateBook(book);
      if (result.status !== 200) {
        toast.error(result.statusText);
        return;
      }      
    },
    onSuccess: () => {
      toast.success("Product has been updated");
      navigate(`/admin-list`);
    }
  });

  return (
    <div className="font-sans">
      <header id="header" className="flex flex-col flex-1">
        <div id="interactions" className="flex gap-4 px-8 py-4">
          <div
            id="container-logo"
            className="flex flex-col items-center text-sm text-[#0a68ff]"
          >
            <img
              src="https://salt.tikicdn.com/ts/upload/0e/07/78/ee828743c9afa9792cf20d75995e134e.png"
              className="w-[96px] h-[40px]"
            />
            <span>Best for you</span>
          </div>
          <div
            id="interaction-panel"
            className="flex flex-1 justify-between items-center gap-4"
          >
            <div
              id="searchbar"
              className="flex flex-row flex-1 rounded-md  border-1 border-gray-300 divide-x divide-gray-300 px-4 py-2 h-fit"
            >
              <div
                id="input-searchbar"
                className="flex flex-1 items-center gap-2 h-8"
              >
                <img
                  className="w-5 h-5"
                  src="https://salt.tikicdn.com/ts/upload/33/d0/37/6fef2e788f00a16dc7d5a1dfc5d0e97a.png"
                ></img>
                <input
                  className="bg-white appearance-none outline-none focus:ring-0 p-2"
                  placeholder="Elysia"
                ></input>
              </div>
              <div className="py-4 bg-gray-300 mx-4 h-8"></div>
              <button className="border-none outline-none focus:ring-0 text-[#0a68ff] h-8">
                Search
              </button>
            </div>
          </div>
        </div>
        <div className="h-[1px] w-full bg-gray-300"></div>
        <nav id="navbar">
          <div className="flex text-sm">
            <button className="h-full py-3 px-12 hover:bg-gray-200 rounded-sm">
              Today's deals
            </button>
            <button className="h-full py-3 px-12 hover:bg-gray-200 rounded-sm">
              Buy Again
            </button>
            <button className="h-full py-3 px-12 hover:bg-gray-200 rounded-sm">
              Gift Cards
            </button>
            <button className="h-full py-3 px-12 hover:bg-gray-200 rounded-sm">
              Sell
            </button>
          </div>
        </nav>
      </header>
      <body
        id="body"
        className="p-4 pt-8 flex flex-row gap-4 bg-[#efefef] min-h-screen"
      >
        <div className="flex flex-col w-full bg-white h-full mx-[6rem] p-4 rounded-md shadow-sm">
          <span className="heading-6 font-bold">Add book</span>
          <div className="flex flex-row justify-between items-center p-6 pt-4">
            <div className="flex flex-col gap-4 p-4 bg-white rounded-md shadow-sm">
              <Input
                title={"Title"}
                placeholder={"Enter title"}
                onChange={onInputChange}
                type={"text"}
                name={"title"}
                value={book?.title}
              />
              <Input
                title={"Price"}
                placeholder={"Enter price"}
                onChange={onInputChange}
                type={"number"}
                name={"price"}
                value={book?.price}
              />
              <Input
                title={"Availability"}
                placeholder={"100"}
                onChange={onInputChange}
                type={"number"}
                name={"availability"}
                value={book?.availability}
              />
              {!isLoadingCategory && categories && (
                <div className="flex flex-col gap-4">
                  <span className="text-sm font-medium leading-5">
                    {"Categories"}
                  </span>
                  <Select
                    name={"name"}
                    className="self-strech w-full"
                    required
                    value={
                      categories?.data.filter((g) => g.id == currentGenreId)
                        .name
                    }
                    onChange={onDropdownChange}
                  >
                    {categories.data.map((item, index) => (
                      <option key={item.id} value={item.id}>
                        {item.name}
                      </option>
                    ))}
                  </Select>
                </div>
              )}
              <button onClick={() => updateProductMutation.mutate(book)} className="bg-blue-500 text-white mt-2 p-4 rounded-lg font-semibold">
                Save
              </button>
            </div>
          </div>
        </div>
      </body>
    </div>
  );
};

export default EditProduct;
