using ConsoleLeetCode;

namespace TestProject2;

public class UnitTest1
{
    private TreeNode MakeTree(int?[] arr)
    {
        Queue<(int index, TreeNode node)> queue = new();
        var root = new TreeNode { val = arr[1]!.Value };
        queue.Enqueue((1, root));

        while (queue.Any())
        {
            var (index, node) = queue.Dequeue();
            var leftIndex = index * 2;
            var rightIndex = index * 2 + 1;
            if (leftIndex < arr.Length && arr[leftIndex] != null)
            {
                var leftNode = new TreeNode { val = arr[leftIndex]!.Value };
                node.left = leftNode;
                queue.Enqueue((leftIndex, leftNode));
            }
            if (rightIndex < arr.Length && arr[rightIndex] != null)
            {
                var rightNode = new TreeNode { val = arr[rightIndex]!.Value };
                node.right = rightNode;
                queue.Enqueue((rightIndex, rightNode));
            }
        }

        return root;
    }

    [Fact]
    public void Test1()
    {
        var tree = MakeTree(new int?[] { null, 1, 2, 3 });
        var sol = new Solution();
        var result = sol.MaxPathSum(tree);

        Assert.Equal(6, result);
    }

    [Fact]
    public void Test2()
    {
        var tree = MakeTree(new int?[] { null, -10, 9, 20, null, null, 15, 7 });
        var sol = new Solution();
        var result = sol.MaxPathSum(tree);

        Assert.Equal(42, result);
    }
}
