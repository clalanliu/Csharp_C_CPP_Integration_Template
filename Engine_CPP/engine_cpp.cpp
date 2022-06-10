#include "engine_cpp.h"
#include "engine.h"

double* AddTwoArrayWIthLenth3_CPP(array_information* arr_info) {
	return AddTwoArrayWIthLenth3(arr_info->a, arr_info->b, &(arr_info->c));
}
