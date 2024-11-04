import { Link, useNavigate } from "react-router-dom";

interface ProductProps {
  product: BookGeneralInfoDTO
}

export function EcommerceProduct(props: ProductProps) {

  const navigate = useNavigate();

  const handleRedirect = () => {
    const defaultState = {
      id: props.product.id,  // Default ID set to 1
      languageCode: "en",  // Default language code
      averageRating: 0,  // Default average rating
      description: props.product.description,  // Default description
      numPages: props.product.numPages,  // Default number of pages
      publicationDay: props.product.publicationDay,  // Default publication day
      publicationMonth: props.product.publicationMonth,  // Default publication month
      publicationYear: props.product.publicationYear,  // Default publication year
      isbn13: 'N/A',  // Default ISBN as a placeholder
      url: 'https://example.com',  // Default URL as a placeholder
      imageUrl: props.product.imageUrl,  // Default image URL as a placeholder
      ratingsCount: props.product.ratingsCount,  // Default ratings count
      title: props.product.title,  // Default title
      titleWithoutSeries: props.product.titleWithoutSeries,  // Default title without series
      price: props.product.price,  // Default price
      availability: 1000,  // Default availability (e.g., in stock)
      dimensions: "2.2 x 2.2 x 2.2 inches",  // Default dimensions as a placeholder
      discountPercentage: props.product.discountPercentage,  // Default discount percentage
      itemWeight: "200 grams",  // Default item weight
      authorName: props.product.authorName,  // Default author name
      formatId: 1,  // Default format ID
      formatName: 'Paperback',  // Default format name
      publisherId: 1,  // Default publisher ID set to 1
      publisherName: 'Elysia x Surgeous',  // Default publisher name
    };
  
    navigate('/product-detail', {
      state: defaultState
    });
  };

  return (
    <div
      className="w-52 p-4 h-fit bg-white 
    rounded-md border-none border-gray-200 flex-col justify-start items-center gap-2.5 hover:shadow-custom-lg hover:shadow-slate-300 inline-flex box-border font-sans"
    onClick={handleRedirect}
    >
      <img
        className="w-48 h-52 rounded-xl cursor-pointer object-cover	"
        src={props.product.imageUrl}
      />
      <div className="px-2 h-full flex flex-col gap-1">
        <div className="line-clamp-2 cursor-pointer h-12 text-black text-md font-normal leading-normal">
          {props.product.title}
        </div>
        <div className="flex items-center gap-3">
          <div className=" text-[#ff424e] text-md font-bold leading-loose">
            {!props.product.discountPercentage
              ? props.product.price
              : (props.product.price * (1 - props.product.discountPercentage)).toFixed(2)}{" "}
            $
          </div>
          {props.product.discountPercentage && (
            <span className="text-black bg-gray-300 text-xs px-1 py-1 font-bold rounded-md">
              -{(props.product.discountPercentage * 100).toFixed(0)}%
            </span>
          )}
        </div>
        {props.product.discountPercentage && (
          <div className="text-black text-sm font-normal line-through leading-tight">
            {props.product.price.toLocaleString()} $
          </div>
        )}
        {!props.product.discountPercentage && <div className="h-4"></div>}
      </div>
      <div className="w-full text-sm text-gray-600 mx-2 border-t-1 py-2 ">
        Delivery at 6/11, Surgeous
      </div>
    </div>
  );
}
