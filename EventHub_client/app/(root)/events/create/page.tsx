import EventForm from "@/components/shared/EventForm";
import { getCurrentUser } from "@/lib/actions/user.actions";
import { NextResponse } from "next/server";

import React from "react";

const CreateEvent = async () => {
  const user = await getCurrentUser();
  console.log("🧪 userType from CreateEvent:", user?.userType);

  if (!user || user.userType !== "host") {
    return (
      <div className="text-center py-10 text-red-500 font-bold">
        Only hosts can create events.
      </div>
    );
  }

  return (
    <>
      <section className="bg-primary-50 bg-dotted-pattern bg-cover bg-center py-5 md:py-1">
        <h3 className="wrapper h3-bold text-center sm:text-left">
          Create Event
        </h3>
      </section>
      <div className="wrapper my-8">
        <EventForm type="Create" />
      </div>
    </>
  );
};

export default CreateEvent;
