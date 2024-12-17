//*****************************************************************************
//** 4Sum                                                           leetcode **
//*****************************************************************************

/**
 * Return an array of arrays of size *returnSize.
 * The sizes of the arrays are returned as *returnColumnSizes array.
 * Note: Both returned array and *columnSizes array must be malloced, assume caller calls free().
 */
int compare(const void* a, const void* b)
{
    return (*(int*)a - *(int*)b);
}

int** fourSum(int* nums, int numsSize, int target, int* returnSize, int** returnColumnSizes)
{
    *returnSize = 0;
    if (numsSize < 4) return NULL;

    // Sort the array
    qsort(nums, numsSize, sizeof(int), compare);

    // Allocate memory for result storage
    int maxResults = 1000; // Initial guess for max number of quadruplets
    int** result = (int**)malloc(maxResults * sizeof(int*));
    *returnColumnSizes = (int*)malloc(maxResults * sizeof(int));

    for (int i = 0; i < numsSize - 3; i++)
    {
        if (i > 0 && nums[i] == nums[i - 1]) continue; // Skip duplicates

        for (int j = i + 1; j < numsSize - 2; j++)
        {
            if (j > i + 1 && nums[j] == nums[j - 1]) continue; // Skip duplicates

            int left = j + 1;
            int right = numsSize - 1;
            long long remaining = (long long)target - nums[i] - nums[j];

            while (left < right)
            {
                long long sum = (long long)nums[left] + nums[right];
                if (sum == remaining)
                {
                    // Allocate space for one quadruplet
                    result[*returnSize] = (int*)malloc(4 * sizeof(int));
                    result[*returnSize][0] = nums[i];
                    result[*returnSize][1] = nums[j];
                    result[*returnSize][2] = nums[left];
                    result[*returnSize][3] = nums[right];
                    (*returnColumnSizes)[*returnSize] = 4;
                    (*returnSize)++;

                    // Expand result memory if needed
                    if (*returnSize == maxResults)
                    {
                        maxResults *= 2;
                        result = (int**)realloc(result, maxResults * sizeof(int*));
                        *returnColumnSizes = (int*)realloc(*returnColumnSizes, maxResults * sizeof(int));
                    }

                    // Move pointers and skip duplicates
                    left++;
                    right--;
                    while (left < right && nums[left] == nums[left - 1]) left++;
                    while (left < right && nums[right] == nums[right + 1]) right--;
                }
                else if (sum < remaining)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }
        }
    }

    return result;
}