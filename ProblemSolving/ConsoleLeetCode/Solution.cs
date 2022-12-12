namespace ConsoleLeetCode;

public class Solution
{
    public int MaxProduct(TreeNode root)
    {
        const long mod = 1_000_000_007;

        long treeSum = root.TreeSum();
        long maxProduct = int.MinValue;

        Queue<TreeNode> queue = new();
        queue.Enqueue(root);

        while (queue.Any())
        {
            var node = queue.Dequeue();
            var nodeSum = node.TreeSum();

            long product = (((treeSum - nodeSum) % mod) * (nodeSum % mod)) % mod;

            maxProduct = Math.Max(maxProduct, product);

            if (node.left != default)
                queue.Enqueue(node.left);
            if (node.right != default)
                queue.Enqueue(node.right);
        }

        return (int)maxProduct;
    }
}

public static class Ex
{
    private static Dictionary<TreeNode, long> _sum = new();
    public static long TreeSum(this TreeNode node)
    {
        if (_sum.TryGetValue(node, out var m))
        {
            return m;
        }

        var leftSum = node.left?.TreeSum() ?? 0;
        var rightSum = node.right?.TreeSum() ?? 0;

        var result = node.val + leftSum + rightSum;
        _sum.Add(node, result);
        return result;
    }
}