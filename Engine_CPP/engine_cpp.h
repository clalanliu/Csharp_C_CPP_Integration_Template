#pragma once

#ifdef ENGINE_EXPORTS
#define ENGINE_API __declspec(dllexport)

struct array_information
{
   double* a;
   double* b;
   double* c;
};

extern "C" ENGINE_API double* AddTwoArrayWIthLenth3_CPP(array_information* arr_info);


#else
#define ENGINE_API __declspec(dllimport)



#endif
